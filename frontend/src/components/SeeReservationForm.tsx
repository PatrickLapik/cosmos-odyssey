import type { UseFormReturn } from "react-hook-form";
import { Button } from "./ui/button";
import { FirstLastNameFields } from "./FirstNameLastNameFields";
import type { SeeReservationFormValues } from "@/schemas/SeeReservationSchema";

type ReservationFormProps = {
  form: UseFormReturn<SeeReservationFormValues>;
};

export const SeeReservationForm = ({ form }: ReservationFormProps) => {
  return (
    <div className="flex flex-col space-y-12 w-full bg-popover px-4 py-6 rounded border h-full">
      <FirstLastNameFields saveToParams control={form.control} />
      <Button type="submit">See your reservations</Button>
    </div>
  );
};
