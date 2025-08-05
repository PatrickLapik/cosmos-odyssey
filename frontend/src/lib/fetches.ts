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

export const fetchReservations = async (values: SeeReservationFormValues): Promise<Reservation[]> => {
    const res = await api.get(`reservations/${values.FirstName}/${values.LastName}`);
    return res.data;
};
