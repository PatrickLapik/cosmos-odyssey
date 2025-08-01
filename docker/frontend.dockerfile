FROM node:24

WORKDIR /frontend

COPY /frontend/package*.json ./

RUN npm i

COPY /frontend/ .
