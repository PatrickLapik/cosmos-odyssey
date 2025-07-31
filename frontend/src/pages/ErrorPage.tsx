import { useRouteError } from "react-router";
import DefaultLayout from "@/layouts/DefaultLayout";

export default function ErrorPage() {
  const error = useRouteError();

  return (
    <DefaultLayout>
      <div className="flex flex-col items-center justify-center text-center px-4 py-20 space-y-4">
        <p className="text-4xl">{error.status}</p>
        <p className="text-lg mb-2">
          Something went wrong or the page doesn't exist.
        </p>
      </div>
    </DefaultLayout>
  );
}
