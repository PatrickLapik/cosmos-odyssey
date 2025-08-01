import path from "path";
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";

const port = process.env.VITE_FRONTEND_PORT
  ? parseInt(process.env.VITE_FRONTEND_PORT)
  : undefined;

// https://vite.dev/config/
export default defineConfig({
  plugins: [
        react(),
        tailwindcss()
    ],

  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
  server: {
    host: true,
    port: port,
  },
});
