import { createBrowserRouter } from "react-router";
import DefaultLayout from "@/layouts/DefaultLayout";
import ErrorPage from "@/pages/ErrorPage";
import HomePage from "@/pages/HomePage";
import RoutesPage from "@/pages/RoutesPage";

export const router = createBrowserRouter([
  {
    Component: DefaultLayout,
    errorElement: <ErrorPage />,
    children: [
      {
        index: true,
        Component: HomePage,
      },
      {
        path: "routes",
        Component: RoutesPage,
      },
    ],
  },
]);
