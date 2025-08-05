import { useLocation } from "react-router";
import type { TravelRoute } from "@/types/ResponseTypes";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { reservationSchema } from "@/schemas/ReservationSchema";
import { ReservationForm } from "@/components/ReservationForm";
import {
  TravelDestination,
  TravelStartEnd,
} from "@/components/TravelRouteCard";
import { PriceListValidTimer } from "@/components/PriceListValidTimer";

export default function MakeReservationPage() {
  const location = useLocation();
  const travelRoute: TravelRoute = location.state?.travelRoute;

  window.scrollTo({ top: 0, left: 0, behavior: "auto" });

  if (!travelRoute) {
    return <p>Invalid access - no travel route data provided.</p>;
  }

  const form = useForm({
    resolver: zodResolver(reservationSchema),
    defaultValues: {
      FirstName: "",
      LastName: "",
      CompanyRouteIds: travelRoute.companyRouteResponses.map((cr) => cr.id),
    },
  });

  return (
    <div className="flex flex-col h- space-x-2 space-y-2">
      <div className="bg-popover rounded border px-4 py-6">
        <PriceListValidTimer />
      </div>

      <div className="flex justify-center w-full h-full space-x-2">
        <ReservationDetails travelRoute={travelRoute} />
        <ReservationForm form={form} />
      </div>
    </div>
  );
}

type ReservationDetailsProps = {
  travelRoute: TravelRoute;
};

const ReservationDetails = ({
  travelRoute,
}: ReservationDetailsProps) => {
  return (
    <div className="flex flex-col bg-popover rounded border px-4 py-6 space-y-6 w-full">
      <p className="w-full text-2xl">
        Trip total price: {travelRoute.totalPrice.toLocaleString()}€
      </p>
      <TravelStartEnd travelRoute={travelRoute} />
      {travelRoute.companyRouteResponses.map((cr) => (
        <TravelDestination route={cr.route} company={cr.company} />
      ))}
    </div>
  );
};
