#!/bin/sh

# Check if .env files exist

if [ ! -f ./backend/.env ]; then
    cp ./backend/.env.example ./backend/.env
fi

if [ ! -f ./frontend/.env ]; then
    cp ./frontend/.env.example ./frontend/.env
fi

set -a
. ./backend/.env
. ./frontend/.env
set +a

if [ "$1" = "db" ]; then
    docker compose --profile "db" up
fi

if [ "$1" = "down" ]; then
    docker compose --profile "db" --profile "backend" --profile "frontend" down 
fi

if [ "$1" = "detached" ]; then
    docker compose --profile "db" --profile "backend" --profile "frontend" up --build -d
fi

if [ "$1" = "npm" ]; then
    npm run dev --prefix frontend
fi

if [ -z "$1" ]; then
    docker compose --profile "db" --profile "backend" --profile "frontend" up --build
fi

