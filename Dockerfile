#FROM node:14-alpine as webapp
#WORKDIR /webapp
#COPY src/Amadeus.WebApp/Front .
#RUN npm install -g @angular/cli
#RUN yarn install --frozen-lockfile
#RUN yarn run build --configuration production

FROM mcr.microsoft.com/dotnet/sdk:5.0 as builder
COPY . .
RUN dotnet publish -c Release -o /usr/lib/amadeus '-p:SkipWebApp=true;CheckCodingStyle=false' src/Amadeus.Host

FROM mcr.microsoft.com/dotnet/aspnet:5.0
EXPOSE 8080
COPY --from=builder /usr/lib/amadeus /usr/lib/amadeus
#COPY --from=webapp /webapp/dist/* /usr/lib/amadeus/wwwroot/
CMD ["/usr/lib/kyoo/amadeus"]
