import { Link } from "react-router";
import Button from "../components/Button";
import { Logo } from "../components/Logo";
import { DefaultLayout } from "../layouts/DefaultLayout";

export default function HomePage() {
  return (
    <DefaultLayout>
      <div className="flex flex-col items-center justify-center text-center px-4 py-20 space-y-10">
        <Logo />
        <h1 className="text-5xl font-bold text-white drop-shadow-md">
          Welcome to Cosmos Odyssey
        </h1>

        <p className="max-w-2xl text-lg text-gray-300">
          Explore the solar system like never before. Compare interplanetary
          travel routes, find the best deals, and reserve your trip to another
          world.
        </p>

        <div className="space-x-4">
          <Button>
            <Link to="/routes">Start exploring</Link>
          </Button>
        </div>
      </div>
    </DefaultLayout>
  );
}
