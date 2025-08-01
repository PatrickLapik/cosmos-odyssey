import { useForm, type Control } from "react-hook-form";
import { RouteFromToSelect } from "./RouteFromToSelect";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  routeFiltersSchema,
  type FormValues,
} from "@/schemas/RouteFiltersSchema";
import { Button } from "./ui/button";
import { Form } from "./ui/form";
import { RouteMaxPrice } from "./RouteMaxPrice";

type RouteFiltersProps = {
  onSubmit: (values: FormValues) => void;
};

export type RouteFilterProps = {
  control: Control<any>;
};

export const RouteFilters = ({ onSubmit }: RouteFiltersProps) => {
  const form = useForm({
    resolver: zodResolver(routeFiltersSchema),
    defaultValues: {
      FromId: "",
      ToId: "",
      MaxPrice: "",
    },
  });

  return (
    <div className="w-1/3 h-fit bg-card border rounded px-4 py-6 sticky top-20 space-y-6">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <RouteFromToSelect control={form.control} />
          <RouteMaxPrice control={form.control} />
          <Button type="submit">Find</Button>
        </form>
      </Form>
    </div>
  );
};
