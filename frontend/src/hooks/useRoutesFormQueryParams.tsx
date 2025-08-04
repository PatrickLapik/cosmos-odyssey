import type { FormValues } from "@/schemas/RouteFiltersSchema";
import { useSearchParams } from "react-router";

export const useRoutesFormQueryParams = () : FormValues => {
  const [params] = useSearchParams();

  return {
    FromId: params.get("FromId") ?? "",
    ToId: params.get("ToId") ?? "",
    MaxPrice: params.get("MaxPrice") ?? undefined,
    CompanyId: params.get("CompanyId") ?? undefined,
    SortBy: params.get("SortBy") ?? "None",
    SortOrder: params.get("SortOrder") ?? "Asc",
  };
};

