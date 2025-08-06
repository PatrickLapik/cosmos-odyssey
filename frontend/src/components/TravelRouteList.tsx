import { TravelRouteCard } from "./TravelRouteCard";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { fetchRoutes } from "@/lib/fetches";
import {
  routeFiltersSchema,
  type FormValues,
} from "@/schemas/RouteFiltersSchema";
import { useEffect } from "react";
import { FetchedContentContainer } from "./FetchedContentContainer";
import { useValidTimer } from "@/providers/ValidTimerProvider";

type TravelRouteListProps = {
  formValues: FormValues;
};

export const TravelRouteList = ({ formValues }: TravelRouteListProps) => {
  const { timeLeft } = useValidTimer();

  const queryClient = useQueryClient();

  const {
    data: travelRoutes,
    isLoading,
    isRefetching,
    refetch,
  } = useQuery({
    queryKey: ["routes", formValues],
    queryFn: () => fetchRoutes(formValues),
    enabled: false,
    staleTime: timeLeft.total,
  });

  useEffect(() => {
    const result = routeFiltersSchema.safeParse(formValues);

    if (result.success) {
      const cached = queryClient.getQueryData(["routes", formValues]);

      if (cached) return;
      void refetch();
    }
  }, [formValues, refetch, queryClient]);

  return (
    <FetchedContentContainer
      isLoading={isLoading || isRefetching}
      data={travelRoutes}
    >
      {travelRoutes?.map((tr, i) => (
        <TravelRouteCard key={i + "Card"} travelRoute={tr} />
      ))}
    </FetchedContentContainer>
  );
};
