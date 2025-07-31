import { Link } from "react-router";
import { Logo } from "./Logo";
import { Button } from "./ui/Button";
import type { PropsWithChildren } from "react";

export const NavBar = () => {
  return (
    <nav className="space-x-4 flex top-0 px-12 py-4 w-full sticky bg-linear-to-b to-slate-950/50 from-slate-900/50 rounded-b-lg z-50">
        <Logo/>
        <NavButton to="/routes">Home</NavButton>
        <NavButton to="/routes">Routes</NavButton>
    </nav>
  );
};

type NavButtonProps = {
  to: string;
};

const NavButton = ({ to, children }: PropsWithChildren<NavButtonProps>) => {
  return (
        <Button asChild variant="link">
            <Link to={to}>{children}</Link>
        </Button>
  );
};
