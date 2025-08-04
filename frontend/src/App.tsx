import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ThemeProvider } from "./providers/ThemeProvider";
import { RouterProvider } from "react-router";
import { router } from "./routes";
import { ValidTimerProvider } from "./providers/ValidTimerProvider";

const queryClient = new QueryClient();

export default function App() {
    return (
        <QueryClientProvider client={queryClient}>
            <ThemeProvider defaultTheme="dark">
                <ValidTimerProvider>
                    <RouterProvider router={router} />
                </ValidTimerProvider>
            </ThemeProvider>
        </QueryClientProvider>
    );
}
