import { z } from "zod";

export const reservationSchema = z
  .object({
    FirstName: z.string().min(1, "First name is required"),
    LastName: z.string().min(1, "Last name is required"),
    CompanyRouteIds: z.array(z.string().nonempty()),
  });

export type ReservationFormValues = z.infer<typeof reservationSchema>;
