import type {
    Company,
    Destination,
    Route,
    TravelRoute,
} from "@/pages/RoutesPage";
import { ArrowRight } from "lucide-react";

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
            <div className="flex flex-col space-y-4">
                {travelStart && travelEnd && (
                    <TravelStartEnd start={travelStart} end={travelEnd} />
                )}
                <div className="flex space-x-2 font-semibold">
                    {travelRoute.companyRouteResponses.map((cr, index, array) => (
                        <TravelDestination
                            key={cr.route.id}
                            route={cr.route}
                            company={cr.company}
                            start={index === 0}
                            end={index === array.length - 1}
                        />
                    ))}
                </div>
            </div>
            <TravelRouteTotals
                totalPrice={travelRoute.totalPrice}
                totalDistance={travelRoute.totalDistance}
                totalMinutes={travelRoute.totalTravelMinutes}
            />
        </div>
    );
};

type TravelStartEndProps = {
    start: Date;
    end: Date;
};

const TravelStartEnd = ({ start, end }: TravelStartEndProps) => {
    return (
        <div className="flex space-x-2">
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
    const commonClasses = "flex space-x-2";

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
        <div className="flex items-center">
            <p className="text-card-foreground font-normal opacity-40 text-xs">
                via {companyName}
            </p>
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
    totalPrice,
    totalDistance,
    totalMinutes,
}: TravelRouteTotalsProps) => {
    return (
        <div className="flex space-x-2 text-popover-foreground opacity-40 text-xs">
            <p>Total trip price: {totalPrice.toLocaleString()}</p>
            <p>Aprox travel time: {(totalMinutes / 60 / 60).toFixed(1)} day(s)</p>
            <p>Total trip distance: {totalDistance.toLocaleString()} km</p>
        </div>
    );
};

const formatDate = (date: Date) => {
    return date.toLocaleString('en-GB', {
        day: '2-digit',
        month: 'short',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        hour12: false,
    }).replace(',', ''); // Remove comma between date and time
};
