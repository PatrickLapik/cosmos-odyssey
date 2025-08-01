import { RouteCard } from "@/components/RouteCard";
import { RouteFilters } from "@/components/RouteFilters";
import api from "@/lib/axios";
import type { FormValues } from "@/schemas/RouteFiltersSchema";
import { useQuery, useQueryClient } from "@tanstack/react-query";

export type Destination = {
  id: string;
  name: string;
};

const fetchDestinations = async (): Promise<Destination[]> => {
  const res = await api.get("destinations");
  return res.data;
};

const fetchRoutes = async (body: FormValues) => {
  const res = await api.post("routes", body);
  return res.data;
};

export default function RoutesPage() {
  const queryClient = useQueryClient();

  const {} = useQuery({
    queryKey: ["destinations"],
    queryFn: fetchDestinations,
    staleTime: "static",
  });

  const onSubmit = async (values: FormValues) => {
    const data = await queryClient.fetchQuery({
      queryKey: ["routes", values],
      queryFn: () => fetchRoutes(values),
    });
  };

  return (
    <div className="flex w-full h-full space-x-6">
      <RouteFilters onSubmit={onSubmit} />
      <div className="flex flex-col w-full space-y-6 py-6 px-4 bg-card rounded border">
        <RouteCard />
        <RouteCard />
        <RouteCard />
        <RouteCard />
        <RouteCard />
        <RouteCard />
        <RouteCard />
        <RouteCard />
        <RouteCard />
        <RouteCard />
        <RouteCard />
      </div>
    </div>
  );
}
