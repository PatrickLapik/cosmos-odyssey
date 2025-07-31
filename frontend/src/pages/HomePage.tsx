import { Link } from "react-router";
import { Logo } from "@/components/Logo";
import { Button } from "@/components/ui/Button";

export default function HomePage() {
  return (
      <div className="flex flex-col items-center justify-center text-center px-4 py-20 space-y-10">
        <Logo />
        <h1 className="text-5xl font-bold text-white drop-shadow-md">
          Welcome to Cosmos Odyssey
        </h1>

        <p className="max-w-2xl text-lg text-gray-300">
            Come and find the best interplanetary travel routes with the best travel companies in this solar system.
        </p>

        <div className="space-x-4">
          <Button asChild>
            <Link className="h-full w-full" to="/routes">Start exploring</Link>
          </Button>
        </div>
      </div>
  );
}
