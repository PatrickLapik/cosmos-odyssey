import { fetchReservations } from "@/lib/fetches";
import { useValidTimer } from "@/providers/ValidTimerProvider";
import {
  seeReservationSchema,
  type SeeReservationFormValues,
} from "@/schemas/SeeReservationSchema";
import { useQuery } from "@tanstack/react-query";
import { useEffect } from "react";
import { safeParse } from "zod/v4/core";
import { FetchedContentContainer } from "./FetchedContentContainer";
import type { Reservation } from "@/types/ResponseTypes";
import { LucideArrowRight } from "lucide-react";

type ReservationListProps = {
  formValues: SeeReservationFormValues;
};

export const ReservationsList = ({ formValues }: ReservationListProps) => {
  const { timeLeft } = useValidTimer();

  const {
    data: travelRoutes,
    isLoading,
    isRefetching,
    refetch,
  } = useQuery({
    queryKey: ["routes", formValues],
    queryFn: () => fetchReservations(formValues),
    enabled: false,
    staleTime: timeLeft.total,
  });

  useEffect(() => {
    const result = safeParse(seeReservationSchema, formValues);

    if (result.success) {
      void refetch();
    }
  }, [formValues, refetch]);

  return (
    <FetchedContentContainer
      isLoading={isLoading || isRefetching}
      data={travelRoutes}
    >
      {travelRoutes?.map((cr, i) => (
        <ReservationDetails key={i} reservation={cr} />
      ))}
    </FetchedContentContainer>
  );
};

const ReservationDetails = ({ reservation }: { reservation: Reservation }) => {
  const routes = reservation.companyRoutes;
  const firstRoute = routes[0];
  const lastRoute = routes[routes.length - 1];
  return (
    <div className="flex flex-col bg-popover rounded border px-4 py-6 h-96">
      <p>
        Reservation for: {reservation.firstName} {reservation.lastName}
      </p>
      <div className="flex space-x-2">
        <p>From: {firstRoute.from}</p>
        <LucideArrowRight />
        <p>To: {lastRoute.to}</p>
      </div>
      <p>Via: {reservation.companyNames.join(" and ")}</p>
      <p>
        Travel time: {(reservation.totalTravelMinutes / 60 / 24).toFixed(1)}{" "}
        day(s)
      </p>
    </div>
  );
};
