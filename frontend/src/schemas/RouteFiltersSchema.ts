import { z } from "zod";

export const routeFiltersSchema = z
  .object({
    FromId: z.string().min(1, "Please select a starting destination"),
    ToId: z.string().min(1, "Please select a destination"),
    MaxPrice: z
      .string()
      .default("")
      .refine((val) => val === "" || /^\d+$/.test(val), {
        message: "Max price must be a valid number",
      })
      .transform((val) => (val === "" ? undefined : parseInt(val, 10))),
  })
  .refine((data) => data.FromId != data.ToId, {
    error: "Start and destination must be different",
    path: ["ToId"],
  });

export type FormValues = z.infer<typeof routeFiltersSchema>;
