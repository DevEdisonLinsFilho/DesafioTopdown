export interface ApiResponse<T> {
    codRetorno: 0 | 1;
    mensagem: string|null;
    data: T;
}