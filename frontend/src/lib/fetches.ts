import type { Company, Destination, TravelRoute } from "@/pages/RoutesPage";
import type { FormValues } from "@/schemas/RouteFiltersSchema";
import api from "./axios";


export const fetchDestinations = async (): Promise<Destination[]> => {
    const res = await api.get("destinations");
    return res.data;
};

export const fetchCompanies = async (): Promise<Company[]> => {
    const res = await api.get("companies");
    return res.data;
};

export const fetchRoutes = async (body: FormValues): Promise<TravelRoute[]> => {
    const res = await api.post("routes", body);
    return res.data;
};
