# Estágio 1: Build da aplicação Angular
FROM node:20-alpine AS build
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build -- --configuration production

# Estágio 2: Servir a aplicação com Nginx
FROM nginx:alpine

# 1. Remove o arquivo de configuração padrão do Nginx para evitar conflitos.
RUN rm /etc/nginx/conf.d/default.conf

# 2. Copia o seu arquivo de configuração customizado.
COPY nginx.conf /etc/nginx/conf.d/default.conf

# 3. Copia os arquivos da aplicação.
COPY --from=build /usr/src/app/dist/frontend/browser /usr/share/nginx/html

RUN ls -la /usr/share/nginx/html

EXPOSE 80