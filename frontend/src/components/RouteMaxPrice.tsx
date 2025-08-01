import type { RouteFormProps } from "./RouteFilters";
import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./ui/form";
import { Input } from "./ui/input";

export const RouteMaxPrice = ({ control }: RouteFormProps) => {
  return (
    <FormField
      control={control}
      name="MaxPrice"
      render={({ field }) => (
        <FormItem>
          <FormLabel>Maximum price:</FormLabel>
          <FormControl>
            <Input
              placeholder="Maximum price"
              inputMode="numeric"
              min="0"
              {...field}
              type="number"
              className="appearance-none"
            />
          </FormControl>
          <FormMessage />
        </FormItem>
      )}
    />
  );
};
