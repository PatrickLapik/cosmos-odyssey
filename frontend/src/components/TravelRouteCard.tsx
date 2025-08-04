import type { Company, Route, TravelRoute } from "@/pages/RoutesPage";
import { ArrowRight } from "lucide-react";
import { Button } from "./ui/button";
import { useNavigate } from "react-router";

type TravelRouteCardProps = {
  travelRoute: TravelRoute;
};

export const TravelRouteCard = ({ travelRoute }: TravelRouteCardProps) => {
  const navigate = useNavigate();
  const handleReserve = () => {
    navigate("reserve", {
      state: { travelRoute },
    });
  };

  return (
    <div className="w-full bg-popover border rounded px-4 py-2 flex flex-col justify-between">
      <div className="flex h-full w-full">
        <div className="flex flex-col space-y-4 w-full h-full justify-between">
          <div className="flex justify-between w-full">
            <TravelStartEnd travelRoute={travelRoute} />
            <b className="w-fit">{travelRoute.totalPrice.toLocaleString()}€</b>
          </div>

          <div className="flex flex-col space-y-2">
            {travelRoute.companyRouteResponses.map((cr, index) => (
              <TravelDestination
                key={cr.route.fromId + cr.route.toId + index}
                route={cr.route}
                company={cr.company}
              />
            ))}
          </div>

          <TravelRouteTotals
            totalPrice={travelRoute.totalPrice}
            totalDistance={travelRoute.totalDistance}
            totalMinutes={travelRoute.totalTravelMinutes}
          />

          <Button onClick={handleReserve} variant="secondary">
            Reserve
          </Button>
        </div>
      </div>
    </div>
  );
};

type TravelStartEndProps = {
  travelRoute: TravelRoute;
};

export const TravelStartEnd = ({ travelRoute }: TravelStartEndProps) => {
  const firstResponse = travelRoute.companyRouteResponses[0];
  const lastResponse =
    travelRoute.companyRouteResponses[
      travelRoute.companyRouteResponses.length - 1
    ];

  const travelStart = new Date(firstResponse.travelStart);
  const travelEnd = new Date(lastResponse.travelEnd);

  return (
    <div className="flex space-x-2 text-chart-1 w-full">
      <p>
        <b>Start:</b> {formatDate(travelStart)}
      </p>
      <p>
        <b>Arrival:</b> {formatDate(travelEnd)}
      </p>
    </div>
  );
};

type TravelDestinationProps = {
  route: Route;
  company: Company;
};

export const TravelDestination = ({
  route,
  company,
}: TravelDestinationProps) => {
  return (
    <div className="flex space-x-2 text-chart-3 items-center">
      <p>From: {route.from}</p>
      <TravelDestinationArrow companyName={company.name} />
      <p>To: {route.to}</p>
    </div>
  );
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
    .replace(",", "");
};
