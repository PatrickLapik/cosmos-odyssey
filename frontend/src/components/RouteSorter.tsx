import {
  Select,
  SelectContent,
  SelectTrigger,
  SelectValue,
  SelectItem,
} from "./ui/select";
import { Form, FormControl, FormField, FormItem, FormLabel } from "./ui/form";
import type { RouteFormProps, RouteFormsProps } from "./RouteFilters";
import { sortByEnum, sortOrderEnum } from "@/schemas/RouteFiltersSchema";

export const RouteSorter = ({ form }: RouteFormsProps) => {
  return (
    <Form {...form}>
      <div className="flex justify-end w-full space-x-6">
        <RouteSortBy control={form.control} />
        <RouteSortOrder control={form.control} />
      </div>
    </Form>
  );
};

const RouteSortBy = ({ control }: RouteFormProps) => {
  return (
    <FormField
      name="SortBy"
      control={control}
      render={({ field }) => (
        <FormItem>
          <Select defaultValue={field.value} onValueChange={field.onChange}>
            <FormLabel>Sort by:</FormLabel>
            <FormControl>
              <SelectTrigger>
                <SelectValue placeholder="SortBy" />
              </SelectTrigger>
            </FormControl>
            <SelectContent>
              {sortByEnum.map((s) => (
                <SelectItem key={s} value={s}>
                  {s}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </FormItem>
      )}
    />
  );
};

const RouteSortOrder = ({ control }: RouteFormProps) => {
  return (
    <FormField
      name="SortOrder"
      control={control}
      render={({ field }) => (
        <FormItem>
          <Select defaultValue={field.value} onValueChange={field.onChange}>
            <FormLabel>Sort order:</FormLabel>
            <FormControl>
              <SelectTrigger>
                <SelectValue placeholder="SortOrder" />
              </SelectTrigger>
            </FormControl>
            <SelectContent>
              {sortOrderEnum.map((s) => (
                <SelectItem key={s} value={s}>
                  {s}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </FormItem>
      )}
    />
  );
};
