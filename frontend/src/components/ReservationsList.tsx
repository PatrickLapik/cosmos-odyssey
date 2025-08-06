import { fetchReservations } from "@/lib/fetches";
import { useValidTimer } from "@/providers/ValidTimerProvider";
import {
  seeReservationSchema,
  type SeeReservationFormValues,
} from "@/schemas/SeeReservationSchema";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useEffect } from "react";
import { safeParse } from "zod/v4/core";
import { FetchedContentContainer } from "./FetchedContentContainer";
import { ReservationDetails } from "@/pages/MakeReservationPage";

type ReservationListProps = {
  formValues: SeeReservationFormValues;
};

export const ReservationsList = ({ formValues }: ReservationListProps) => {
  const { timeLeft } = useValidTimer();

  const queryClient = useQueryClient();

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
      const cached = queryClient.getQueryData(["routes", formValues]);
      if (cached) return;
      void refetch();
    }
  }, [formValues, refetch]);

  return (
    <FetchedContentContainer
      isLoading={isLoading || isRefetching}
      data={travelRoutes}
    >
      {travelRoutes?.map((tr, i) => (
        <ReservationDetails key={i} travelRoute={tr} />
      ))}
    </FetchedContentContainer>
  );
};
