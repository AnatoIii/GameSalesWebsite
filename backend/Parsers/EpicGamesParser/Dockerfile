FROM node:latest as BUILD
WORKDIR /app
COPY package.json .
RUN npm install
COPY . .
RUN cat appsettings.json
RUN node parser.js