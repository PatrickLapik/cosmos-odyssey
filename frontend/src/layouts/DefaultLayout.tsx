import type React from "react";
import { NavBar } from "../components/NavBar";
import { Footer } from "../components/Footer";

type DefaultLayoutProps = {
    children: React.ReactNode 
}

export const DefaultLayout : React.FC<DefaultLayoutProps> = ({ children }) => {
    return (
        <div className="flex flex-col w-full h-full items-center justify-between">
            <NavBar />
            <div className="px-12 py-8 w-full min-h-9/10">
                {children}
            </div>
            <Footer />
        </div>
    );
}
