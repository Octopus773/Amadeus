FROM node:14-alpine AS builder
WORKDIR /webapp
COPY package.json .
COPY yarn.lock .
RUN yarn install --frozen-lockfile
COPY . .
RUN yarn run build --configuration production
FROM nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /webapp/dist/host /usr/share/nginx/html
