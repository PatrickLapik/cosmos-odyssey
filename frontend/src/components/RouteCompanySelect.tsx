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
import { useQueryClient } from "@tanstack/react-query";
import type { Company } from "@/pages/RoutesPage";
import type { RouteFormProps } from "./RouteFilters";

export const RouteCompanySelect = ({ control }: RouteFormProps) => {
  const queryClient = useQueryClient();

  const companies: Company[] | undefined = queryClient.getQueryData([
    "companies",
  ]);

  return (
    <FormField
      name="CompanyId"
      control={control}
      render={({ field }) => (
        <FormItem>
          <FormLabel>Company:</FormLabel>
          <Select defaultValue={field.value} onValueChange={field.onChange}>
            <FormControl>
              <SelectTrigger className="w-full">
                <SelectValue placeholder="Company" />
              </SelectTrigger>
            </FormControl>
            <SelectContent>
              <SelectItem value={" "}>None</SelectItem>
              {companies?.map((c) => (
                <SelectItem key={c.id} value={c.id}>
                  {c.name}
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
  );
};
