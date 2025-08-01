import { NavBar } from "../components/NavBar";
import { Footer } from "../components/Footer";
import { Outlet } from "react-router";

export default function DefaultLayout({ children }: React.PropsWithChildren) {
  return (
    <div className="flex flex-col w-full h-full items-center justify-between">
      <NavBar />
      <div className="px-48 py-8 w-full min-h-9/10">
        <Outlet />
        {children}
      </div>
      <Footer />
    </div>
  );
}
