#!/bin/sh

set -a
. ./backend/.env
set +a

dotnet restore ./backend

docker compose up --build
