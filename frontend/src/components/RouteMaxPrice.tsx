import type { RouteFilterProps } from "./RouteFilters";
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from "./ui/form";
import { Input } from "./ui/input";

export const RouteMaxPrice = ({ control }: RouteFilterProps) => {
    return (
    <FormField
      control={control}
      name="MaxPrice"
      render={({ field }) => (
        <FormItem>
          <FormLabel>Maximum price:</FormLabel>
          <FormControl>
            <Input placeholder="..." {...field} />
          </FormControl>
          <FormMessage />
        </FormItem>
      )}
    />
    );
};
