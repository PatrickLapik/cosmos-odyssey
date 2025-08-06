# Cosmos Odyssey

## Running

Make sure you have **docker** and **docker compose** or **docker desktop** installed.

The projects run script can be only run on UNIX-like shells.

If you are on Windows you should run the project from **WSL** or something that can run **Bash** scripts (like Git Bash).

### Clone repo

```
git clone https://github.com/PatrickLapik/cosmos-odyssey.git
```

### Run script
Run the run.sh script in the project root:
```
./run.sh
```

### If running script is not possible

 1. Copy the `./backend/.env.example` to `./backend/.env`
 2. Copy the `./frontend/.env.example` to `./frontend/.env`
 3. Merge the two `.env` files into one, placing it in the project root directory:

 Merged file should include
```
# Filename: .merged.env

# --- Frontend environment variables ---

# --- Backend environment variables ---

```

4. Assuming you named the merged env file to `.merged.env` and it's in project root run in the project root:
```
docker compose --env-file .merged.env --profile "db" --profile "backend" --profile "frontend" up --build
```

## After running

Go to:
```
http://localhost:3000
```
