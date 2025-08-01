import { type Control, type UseFormReturn } from "react-hook-form";
import { RouteFromToSelect } from "./RouteFromToSelect";
import {
  type FormValues,
} from "@/schemas/RouteFiltersSchema";
import { Form } from "./ui/form";
import { RouteMaxPrice } from "./RouteMaxPrice";
import { RouteCompanySelect } from "./RouteCompanySelect";

export type RouteFormsProps = {
  form: UseFormReturn<FormValues>;
};

export type RouteFormProps = {
  control: Control<any>;
};

export const RouteFilters = ({ form }: RouteFormsProps) => {
  return (
    <Form {...form}>
      <RouteFromToSelect control={form.control} />
      <div className="flex flex-col space-y-12">
        {/* <RouteMaxPrice control={form.control} /> */}
        <RouteCompanySelect control={form.control} />
      </div>
    </Form>
  );
};
