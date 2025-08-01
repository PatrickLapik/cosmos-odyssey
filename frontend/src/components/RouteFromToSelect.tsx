import { useQueryClient } from "@tanstack/react-query";
import {
  Select,
  SelectContent,
  SelectTrigger,
  SelectValue,
  SelectItem,
} from "./ui/select";
import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./ui/form";
import type { Control } from "react-hook-form";
import type { Destination } from "@/pages/RoutesPage";

type RouteFromToSelectProps = {
  control: Control<any>;
};

export const RouteFromToSelect = ({ control }: RouteFromToSelectProps) => {
  const queryClient = useQueryClient();

  const destinations: Destination[] | undefined = queryClient.getQueryData(["destinations"]);

  const commonClasses = "w-54";

  return (
    <div className="flex justify-between">
      {/* from */}
      <FormField
        name="FromId"
        control={control}
        render={({ field }) => (
          <FormItem className={commonClasses}>
            <FormLabel>From:</FormLabel>
            <Select defaultValue={field.value} onValueChange={field.onChange}>
              <FormControl>
                <SelectTrigger className={commonClasses}>
                  <SelectValue placeholder="From" />
                </SelectTrigger>
              </FormControl>
              <SelectContent>
                {destinations?.map((d) => (
                  <SelectItem key={d.id} value={d.id}>
                    {d.name}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
            <div className="min-h-12">
              <FormMessage className="text-wrap" />
            </div>
          </FormItem>
        )}
      />

      {/* to */}
      <FormField
        name="ToId"
        control={control}
        render={({ field }) => (
          <FormItem className={commonClasses + " justify-end"}>
            <FormLabel>To:</FormLabel>
            <Select defaultValue={field.value} onValueChange={field.onChange}>
              <FormControl>
                <SelectTrigger className={commonClasses}>
                  <SelectValue placeholder="To" />
                </SelectTrigger>
              </FormControl>
              <SelectContent>
                {destinations?.map((d) => (
                  <SelectItem key={d.id} value={d.id}>
                    {d.name}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
            <div className="min-h-12">
              <FormMessage className="text-wrap" />
            </div>
          </FormItem>
        )}
      />
    </div>
  );
};
