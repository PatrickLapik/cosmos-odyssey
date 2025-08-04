import type {
    Company,
    Route,
    TravelRoute,
} from "@/pages/RoutesPage";
import { ArrowRight } from "lucide-react";
import { Button } from "./ui/button";

type TravelRouteCardProps = {
    travelRoute: TravelRoute;
};

export const TravelRouteCard = ({ travelRoute }: TravelRouteCardProps) => {
    const firstResponse = travelRoute.companyRouteResponses[0];
    const lastResponse =
        travelRoute.companyRouteResponses[
        travelRoute.companyRouteResponses.length - 1
        ];

    const travelStart = firstResponse?.travelStart
        ? new Date(firstResponse.travelStart)
        : null;
    const travelEnd = lastResponse?.travelEnd
        ? new Date(lastResponse.travelEnd)
        : null;

    return (
        <div className="w-full h-44 bg-popover border rounded px-4 py-2 flex flex-col justify-between">
            <div className="flex h-full w-full">
                <div className="flex flex-col space-y-4 w-full h-full justify-between">
                    {travelStart && travelEnd && (
                        <TravelStartEnd start={travelStart} end={travelEnd} />
                    )}
                    <div className="flex space-x-2">
                        {travelRoute.companyRouteResponses.map((cr, index, array) => (
                            <TravelDestination
                                key={cr.route.fromId + cr.route.toId + index}
                                route={cr.route}
                                company={cr.company}
                                start={index === 0}
                                end={index === array.length - 1}
                            />
                        ))}
                    </div>

                    <TravelRouteTotals
                        totalPrice={travelRoute.totalPrice}
                        totalDistance={travelRoute.totalDistance}
                        totalMinutes={travelRoute.totalTravelMinutes}
                    />
                </div>
                <div className="flex flex-col justify-between w-34 text-center">
                    <b className="w-full">{travelRoute.totalPrice.toLocaleString()} €</b>
                    <Button variant="secondary">Reserve</Button>
                </div>
            </div>
        </div>
    );
};

type TravelStartEndProps = {
    start: Date;
    end: Date;
};

const TravelStartEnd = ({ start, end }: TravelStartEndProps) => {
    return (
        <div className="flex space-x-2 text-chart-1">
            <p>
                <b>Start:</b> {formatDate(start)}
            </p>
            <p>
                <b>Arrival:</b> {formatDate(end)}
            </p>
        </div>
    );
};

type TravelDestinationProps = {
    route: Route;
    company: Company;
    start?: boolean;
    end?: boolean;
};

const TravelDestination = ({
    route,
    company,
    start = false,
    end = false,
}: TravelDestinationProps) => {
    const commonClasses = "flex space-x-2 text-chart-3";

    if (start && end) {
        return (
            <div className={commonClasses}>
                <p>From: {route.from}</p>
                <TravelDestinationArrow companyName={company.name} />
                <p>To: {route.to}</p>
            </div>
        );
    } else if (start) {
        return (
            <div className={commonClasses}>
                <p>From: {route.from}</p>
                <TravelDestinationArrow companyName={company.name} />
            </div>
        );
    } else if (end) {
        return (
            <div className={commonClasses}>
                <p>To: {route.to}</p>
            </div>
        );
    } else {
        return (
            <div className={commonClasses}>
                <p>{route.from}</p>
                <TravelDestinationArrow companyName={company.name} />
            </div>
        );
    }
};

const TravelDestinationArrow = ({ companyName }: { companyName: string }) => {
    return (
        <div className="flex items-center space-x-2">
            <p className="font-normal opacity-40 text-xs">via {companyName}</p>
            <ArrowRight />
        </div>
    );
};

type TravelRouteTotalsProps = {
    totalPrice: number;
    totalDistance: number;
    totalMinutes: number;
};

const TravelRouteTotals = ({
    totalDistance,
    totalMinutes,
}: TravelRouteTotalsProps) => {
    return (
        <div className="flex space-x-2 opacity-40 text-chart-3 text-xs">
            <p>Travel time: {(totalMinutes / 60 / 24).toFixed(1)} day(s)</p>
            <p>Total trip distance: {totalDistance.toLocaleString()} km</p>
        </div>
    );
};

const formatDate = (date: Date) => {
    return date
        .toLocaleString("en-GB", {
            day: "numeric",
            month: "short",
            year: "numeric",
            hour: "2-digit",
            minute: "2-digit",
            hour12: false,
        })
        .replace(",", ""); // Remove comma between date and time
};
