# Angular 17 SSR dockerfile
FROM node:20-alpine as angular
WORKDIR /home/app
COPY package*.json .
RUN npm ci
COPY . .
RUN npm run build

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=angular /home/app/dist/microservices-starter-spa/browser .
EXPOSE 80
