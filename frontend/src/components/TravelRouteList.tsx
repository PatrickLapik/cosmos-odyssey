import type { TravelRoute } from "@/pages/RoutesPage";
import { TravelRouteCard } from "./TravelRouteCard";

type TraveRouteListProps = {
    travelRoutes?: TravelRoute[];
};

export const TravelRouteList = ({ travelRoutes }: TraveRouteListProps) => {
    return (
        <div className="flex flex-col w-full space-y-6 py-6 px-4 bg-card rounded border">

            {travelRoutes ? ( travelRoutes?.map((tr, i) => (
                <TravelRouteCard key={i + "Card"} travelRoute={tr} />
            ))) : (
                <p className="w-full text-center">No travel routes...</p>
            )}
        </div>
    );
};
