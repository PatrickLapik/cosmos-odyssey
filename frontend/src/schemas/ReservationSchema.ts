import { useValidTimer } from "@/providers/ValidTimerProvider";
import { z } from "zod";

export const reservationSchema = z
  .object({
    FirstName: z.string(),
    LastName: z.string(),
    CompanyRouteIds: z.array(z.string().nonempty()),
  });

export type ReservationFormValues = z.infer<typeof reservationSchema>;
