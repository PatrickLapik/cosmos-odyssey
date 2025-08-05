import type { ReservationFormValues } from "@/schemas/ReservationSchema";
import type { UseFormReturn } from "react-hook-form";
import {
  Form,
  FormField,
  FormItem,
  FormMessage,
} from "./ui/form";
import { Button } from "./ui/button";
import api from "@/lib/axios";
import { useApiErrors } from "@/hooks/useApiErrors";
import { toast } from "sonner";
import { useNavigate } from "react-router";
import { FirstLastNameFields } from "./FirstNameLastNameFields";

type ReservationFormProps = {
  form: UseFormReturn<ReservationFormValues>;
};

export const ReservationForm = ({ form }: ReservationFormProps) => {
  const { handleErrors } = useApiErrors();
  const navigate = useNavigate();

  const handleReservation = async (values: ReservationFormValues) => {
    try {
      await api.post("reservations", values);
      toast.success("Reservation created successfully!");
      navigate("/");
    } catch (err: any) {
      const apiErrors = err.response?.data;

      if (apiErrors?.errors && err.response.status === 400) {
        handleErrors<ReservationFormValues>(apiErrors, form.setError);
      } else {
        toast.error("Unexpected error occurred");
      }
    }
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(handleReservation)} className="w-full">
        <div className="flex flex-col space-y-6 w-96 bg-popover px-4 py-6 rounded border w-full h-full">
          <FirstLastNameFields control={form.control} />
          <FormField
            control={form.control}
            name="CompanyRouteIds"
            render={() => (
              <FormItem>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="root"
            render={() => (
              <FormItem>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button type="submit">Reserve</Button>
        </div>
      </form>
    </Form>
  );
};
