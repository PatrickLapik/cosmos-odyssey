import { z } from "zod";

export const routeFiltersSchema = z
.object({
    FromId: z.string().min(1, "Please select a starting destination"),
    ToId: z.string().min(1, "Please select a destination"),
})
.refine((data) => data.FromId != data.ToId, {
    error: "Start and destination must be different",
    path: ["ToId"],
});

export type FormValues = z.infer<typeof routeFiltersSchema>;
