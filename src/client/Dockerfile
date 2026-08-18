FROM node:24-alpine AS build
WORKDIR /source
COPY src/client/package.json src/client/package-lock.json ./
RUN npm ci
COPY src/client/ ./
RUN npm run build

FROM nginx:1.29-alpine AS final
COPY src/client/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /source/dist/client/browser /usr/share/nginx/html
EXPOSE 80
