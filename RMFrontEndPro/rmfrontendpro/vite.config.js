import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
    base: '/',
    build: {
        outDir: 'build', 
    },
    plugins: [plugin()],
    server: {
        port: 8080,
        open: true,
    }
})
