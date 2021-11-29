FROM node:14-alpine AS builder
WORKDIR /webapp
COPY package.json .
COPY yarn.lock .
RUN yarn install --frozen-lockfile
COPY . .
RUN yarn run build --configuration production

FROM mpsorg/sass-compiler AS login
WORKDIR /login
COPY projects/login .
RUN sass "style.scss:style.css"
RUN rm style.scss
RUN sed -i 's@<base href="/">@<base href="/login/">@' index.html

FROM nginx
COPY --from=builder /webapp/dist/host /usr/share/nginx/html
COPY --from=login /login /usr/share/nginx/html/login
