import type { Company, Destination, Reservation, TravelRoute } from "@/types/ResponseTypes";
import type { FormValues } from "@/schemas/RouteFiltersSchema";
import api from "./axios";
import type { SeeReservationFormValues } from "@/schemas/SeeReservationSchema";


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

export const fetchReservations = async (body: SeeReservationFormValues): Promise<Reservation[]> => {
    const res = await api.post("reservations/show", body);
    return res.data;
};
