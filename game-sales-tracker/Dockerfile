FROM node:latest as BUILD
WORKDIR /app
COPY package.json .
RUN npm install
COPY . .
RUN npm run build

FROM nginx:latest as DEPLOY
COPY --from=BUILD /app/dist/game-sales-tracker /usr/share/nginx/html
EXPOSE 80
CMD ["nginx","-g","daemon off;"]
