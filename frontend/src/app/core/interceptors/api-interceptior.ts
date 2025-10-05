import { HttpInterceptorFn, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, map, throwError } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiResponse } from '../../core/models/api-response.model';

export const apiInterceptor: HttpInterceptorFn = (req, next) => {
  const snackBar = inject(MatSnackBar);

  return next(req).pipe(
    map((event) => {
      if (
        event instanceof HttpResponse &&
        event.body &&
        typeof event.body === 'object' &&
        'codRetorno' in event.body
      ) {
        const apiResponse = event.body as ApiResponse<any>;

        if (apiResponse.codRetorno === 1) {
          throw new Error(apiResponse.mensagem ?? 'Ocorreu um erro de negócio.');
        }

        return event.clone({ body: apiResponse.data });
      }
      
      return event;
    }),
    catchError((error) => {
      let errorMessage = 'Ocorreu um erro desconhecido. Tente novamente mais tarde.';

      if (error instanceof HttpErrorResponse) {
        const apiError = error.error as ApiResponse<any>;
        if (apiError && apiError.mensagem) {
          errorMessage = apiError.mensagem;
        } else {
          errorMessage = `Erro de conexão: ${error.status}`;
        }
      } else if (error instanceof Error) {
        errorMessage = error.message;
      }

      snackBar.open(errorMessage, 'Fechar', {
        duration: 5000,
        panelClass: ['error-snackbar']
      });

      return throwError(() => new Error(errorMessage));
    })
  );
};