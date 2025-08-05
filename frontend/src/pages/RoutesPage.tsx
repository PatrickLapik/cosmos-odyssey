import { PriceListValidTimer } from "@/components/PriceListValidTimer";
import { RouteFilters } from "@/components/RouteFilters";
import { RouteSorter } from "@/components/RouteSorter";
import { Button } from "@/components/ui/button";
import {
  routeFiltersSchema,
  type FormValues,
} from "@/schemas/RouteFiltersSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useQuery } from "@tanstack/react-query";
import { useForm } from "react-hook-form";
import { TravelRouteList } from "@/components/TravelRouteList";
import { useFormQueryParams } from "@/hooks/useFormQueryParams";
import { fetchCompanies, fetchDestinations } from "@/lib/fetches";
import { useState } from "react";
import { useValidTimer } from "@/providers/ValidTimerProvider";


export default function RoutesPage() {
  const { result: paramValues } = useFormQueryParams<FormValues>();
  const { timeLeft, query: validTimerQuery } = useValidTimer();

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

  const form = useForm({
    resolver: zodResolver(routeFiltersSchema),
    defaultValues: {
      FromId: paramValues.FromId ?? "",
      ToId: paramValues.ToId ?? "",
      CompanyId: paramValues.CompanyId ?? undefined,
      MaxPrice: undefined,
      SortBy: paramValues.SortBy ?? "None",
      SortOrder: paramValues.SortOrder ?? "Asc",
    },
  });

  const [formValues, setFormValues] = useState<FormValues>(form.getValues());

  const onSubmit = async (values: FormValues) => {
    if (
      JSON.stringify(formValues) !== JSON.stringify(values) ||
      timeLeft.total <= 0
    ) {
      setFormValues(values);
      validTimerQuery.refetch();
    }
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
            <Button
              type="submit"
              onClick={form.handleSubmit(onSubmit)}
              className="w-full"
            >
              Find best routes
            </Button>
          </div>
          <TravelRouteList formValues={formValues} />
        </div>
      </div>
    </div>
  );
}
