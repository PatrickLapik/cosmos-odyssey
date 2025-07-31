import { Link } from "react-router";
import Button from "./Button";
import { Logo } from "./Logo";

export const NavBar = () => {
  return (
    <nav className="space-x-4 flex top-0 px-12 py-4 w-full sticky bg-linear-to-b to-slate-950/50 from-slate-900/50 rounded-b-lg z-50">
        <Logo/>
        <Button variant="no_bg_underline">
            <Link to='/'>Home</Link>
        </Button>
        <Button variant="no_bg_underline">
            <Link to='/routes'>Routes</Link>
        </Button>
    </nav>
  );
};
