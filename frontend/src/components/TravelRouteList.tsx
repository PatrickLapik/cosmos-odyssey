import { TravelRouteCard } from "./TravelRouteCard";
import { useQuery } from "@tanstack/react-query";
import { fetchRoutes } from "@/lib/fetches";
import {
  routeFiltersSchema,
  type FormValues,
} from "@/schemas/RouteFiltersSchema";
import { useEffect } from "react";
import { FetchedContentContainer } from "./FetchedContentContainer";

type TravelRouteListProps = {
  formValues: FormValues;
};

export const TravelRouteList = ({ formValues }: TravelRouteListProps) => {
  const {
    data: travelRoutes,
    isLoading,
    isRefetching,
    refetch,
  } = useQuery({
    queryKey: ["routes", formValues],
    queryFn: () => fetchRoutes(formValues),
    enabled: false,
  });

  useEffect(() => {
    const result = routeFiltersSchema.safeParse(formValues);

    if (result.success) {
      void refetch();
    }
  }, [formValues, refetch]);

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
