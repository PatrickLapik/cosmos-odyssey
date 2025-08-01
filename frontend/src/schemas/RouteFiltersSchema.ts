import { z } from "zod";

export const sortByEnum = ["None", "Price", "TraveTime", "Distance"] as const;
export const sortOrderEnum = ["Asc", "Desc"] as const;

export const routeFiltersSchema = z
  .object({
    FromId: z.string().min(1, "Please select a starting destination"),
    ToId: z.string().min(1, "Please select a destination"),
    MaxPrice: z
      .string()
      .default("")
      .refine((val) => val === "" || /^\d+$/.test(val.toString()), {
        message: "Must be a valid number",
      })
      .transform((val) => (val === "" ? undefined : val))
      .optional(),
    CompanyId: z
      .string()
      .default("")
      .transform((val) => (val.trim() === "" ? undefined : val))
      .optional(),
    SortBy: z.enum(sortByEnum).default("None").optional(),
    SortOrder: z.enum(sortOrderEnum).default("Asc").optional(),
  })
  .refine((data) => data.FromId != data.ToId, {
    error: "Start and end destination must be different",
    path: ["ToId"],
  });

export type FormValues = z.infer<typeof routeFiltersSchema>;
