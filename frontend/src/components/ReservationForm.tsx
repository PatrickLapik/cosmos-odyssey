import type { ReservationFormValues } from "@/schemas/ReservationSchema";
import type { UseFormReturn } from "react-hook-form";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./ui/form";
import { Input } from "./ui/input";
import { Button } from "./ui/button";
import api from "@/lib/axios";
import { useApiErrors } from "@/hooks/useApiErrors";
import { toast } from "sonner";
import { useNavigate } from "react-router";

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
          <FormField
            control={form.control}
            name="FirstName"
            render={({ field }) => (
              <FormItem>
                <FormLabel>First name</FormLabel>
                <FormControl>
                  <Input placeholder="John" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          ></FormField>

          <FormField
            control={form.control}
            name="LastName"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Last name</FormLabel>
                <FormControl>
                  <Input placeholder="Doe" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          ></FormField>
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
