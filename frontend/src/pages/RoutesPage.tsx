import { PriceListValidTimer } from "@/components/PriceListValidTimer";
import { RouteCard } from "@/components/RouteCard";
import { RouteFilters } from "@/components/RouteFilters";
import { RouteSorter } from "@/components/RouteSorter";
import { Button } from "@/components/ui/button";
import api from "@/lib/axios";
import {
  routeFiltersSchema,
  type FormValues,
} from "@/schemas/RouteFiltersSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useForm } from "react-hook-form";

export type Destination = {
  id: string;
  name: string;
};

export type Company = {
  id: string;
  name: string;
};

export type ValidUntil = {
  validUntil: Date | string;
};

const fetchDestinations = async (): Promise<Destination[]> => {
  const res = await api.get("destinations");
  return res.data;
};

const fetchCompanies = async (): Promise<Company[]> => {
  const res = await api.get("companies");
  return res.data;
};

const fetchRoutes = async (body: FormValues) => {
  const res = await api.post("routes", body);
  return res.data;
};

const fetchValidUntil = async (): Promise<ValidUntil> => {
  const res = await api.get("travel-prices/valid-until");
  return res.data;
};

export default function RoutesPage() {
  const queryClient = useQueryClient();

  useQuery({
    queryKey: ["destinations"],
    queryFn: fetchDestinations,
    staleTime: "static",
  });

  useQuery({
    queryKey: ["companies"],
    queryFn: fetchCompanies,
    staleTime: "static",
  });

  useQuery({
    queryKey: ["validUntil"],
    queryFn: fetchValidUntil,
  });

  const form = useForm({
    resolver: zodResolver(routeFiltersSchema),
    defaultValues: {
      FromId: "",
      ToId: "",
      CompanyId: "",
      MaxPrice: "",
      SortBy: "None",
      SortOrder: "Asc",
    },
  });

  const onSubmit = async (values: FormValues) => {
    const data = await queryClient.fetchQuery({
      queryKey: ["routes", values],
      queryFn: () => fetchRoutes(values),
    });
  };

  return (
    <div className="flex w-full h-full space-x-6">
      <div className="flex flex-col w-full space-y-6">
        <div className="h-fit bg-card border rounded px-4 py-6 flex items-center">
          <PriceListValidTimer />
          <RouteSorter form={form} />
        </div>
        <div className="flex space-x-6">
          <div className="w-1/3 h-fit bg-card border rounded px-4 py-6 sticky top-20 space-y-6">
            <RouteFilters form={form} />
            <Button type="submit" onClick={form.handleSubmit(onSubmit)} className="w-full">
              Find best routes
            </Button>
          </div>
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
      </div>
    </div>
  );
}
