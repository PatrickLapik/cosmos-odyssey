import { z } from "zod";

export const seeReservationSchema = z
  .object({
    FirstName: z.string().min(1, "First name is required"),
    LastName: z.string().min(1, "Last name is required"),
  });

export type SeeReservationFormValues = z.infer<typeof seeReservationSchema>;
