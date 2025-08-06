import { ReservationsList } from "@/components/ReservationsList";
import { SeeReservationForm } from "@/components/SeeReservationForm";
import { Form } from "@/components/ui/form";
import { useFormQueryParams } from "@/hooks/useFormQueryParams";
import {
  type SeeReservationFormValues,
  seeReservationSchema,
} from "@/schemas/SeeReservationSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react";
import { useForm } from "react-hook-form";

export function SeeReservationsPage() {
  const param = useFormQueryParams<SeeReservationFormValues>(["FirstName", "LastName"]);

  const form = useForm<SeeReservationFormValues>({
    resolver: zodResolver(seeReservationSchema),
    defaultValues: {
      FirstName: param.FirstName ?? "",
      LastName: param.LastName ?? "",
    },
  });

  const [formValues, setFormValues] = useState<SeeReservationFormValues>(param);

  const handleSeeing = async (values: SeeReservationFormValues) => {
    if (JSON.stringify(formValues) !== JSON.stringify(values)) {
      setFormValues(values);
    }
  };

  return (
    <div className="flex space-x-4 w-full">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(handleSeeing)} className="w-1/3 h-full sticky top-20">
          <SeeReservationForm form={form} />
        </form>
      </Form>
      <ReservationsList formValues={formValues} />
    </div>
  );
}
