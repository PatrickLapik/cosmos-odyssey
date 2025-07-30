import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

const port = process.env.VITE_FRONTEND_PORT ? parseInt(process.env.VITE_FRONTEND_PORT) : undefined;

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
        host: true,
        port: port,
    },
});
